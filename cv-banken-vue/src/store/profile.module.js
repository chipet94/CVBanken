import profileService from "@/services/profileService";

import User from "@/models/User";

const initialState = {
    selectedProfile: User.prototype,
    profiles: [],
    userProfile: {}
}

export const profile = {
    namespaced: true,
    state: initialState,
    getters: {
        getProfile: state => (url) => {
            return state.profiles.find(p => p.url === url)
        },
    },
    actions: {
        getAllProfiles() {
            return profileService.getAllProfiles().then(
                profiles => {
                    console.log(profiles)
                    return Promise.resolve(profiles.data)
                },
                err => {
                    console.log(err);
                    return Promise.reject(err)
                }
            )
        },
        getUserProfile({commit}) {
            return profileService.getCurrentUserProfile().then(
                profile => {
                    commit('userProfileSuccess', profile.data);
                    return Promise.resolve(profile.data);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        getByUrl({commit, dispatch}, url) {
            return profileService.getProfile(url).then(
                profile => {
                    commit("userProfileSuccess", profile.data)
                    dispatch("getProfilePicture", new User(profile.data).id)
                    return Promise.resolve(profile.data);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        updateProfile({commit, state, dispatch}, request) {
            return profileService.updateProfile(state.selectedProfile.id, request).then(
                profile => {
                    dispatch("getByUrl", state.selectedProfile.url).then(commit("profileUpdated"))
                    commit("userProfileSuccess");
                    return Promise.resolve(profile.data);
                },
                error => {
                    commit("profileUpdateFail")
                    return Promise.reject(error)
                }
            )
        },
        getProfilePicture({commit}, id) {
            return profileService.getProfilePicture(id).then(
                picture => {
                    commit("profilePictureFetchSuccess", {data: picture.data, id})
                    return Promise.resolve(picture)
                },
                err => {
                    return Promise.reject(err)
                }
            )
        },
        getUserFiles({commit}, id) {
            return profileService.getUserFiles(id).then(
                files => {
                    commit("userFilesSuccess", {data: files.data, id})
                    return Promise.resolve(files)
                },
                err => {
                    return Promise.reject(err)
                }
            )
        },
        updateProfilePicture({commit, state, dispatch}, request) {
            return profileService.updateProfilePicture(state.selectedProfile.id, request).then(
                res => {
                    commit("profilePictureUpdated");
                    dispatch("getProfilePicture", state.selectedProfile.id)
                    return Promise.resolve(res);
                },
                error => {
                    commit("profileUpdateFail")
                    return Promise.reject(error.response)
                }
            )
        }
    },
    mutations: {
        async userProfileSuccess(state, profile) {
            let user = new User(profile);
            state.selectedProfile = user;
            let target = state.profiles.find(prof => {
                return prof.id === user.id;
            });
            target ? Object.assign(target, user) : state.profiles.push(user);
            console.log(state.profiles)
        },
        userFilesSuccess(state, {data, id}) {
            let target = state.profiles.find(prof => {
                return prof.id === id;
            });
            if (target) {
                target.files = data
            }
        },
        userProfileFailure(state) {
            state.selectedProfile = null;
        },
        async profilePictureFetchSuccess(state, {data, id}) {
            if (data.size > 1) {
                let target = state.profiles.find(prof => prof.id === id);
                if (target !== undefined) {
                    await target.setProfilePicture(data)
                }
            }

            //await user.setProfilePicture(picture);
            // let tar = state.profiles.find(p => p.url === url)
            // console.log("before", tar)
            // tar? Object.assign(tar.profilePicture, picture): state.profiles.push(new User({url: url, profilePicture: picture}))
            // console.log("after", tar)
        },
        async profileUpdated(state) {
            await state.selectedProfile.getProfilePicture()
        },
        profileUpdateFail(state, profile) {

            state.selectedProfile = {...state.selectedProfile, profile}
        },
        async profilePictureUpdated(state) {
            await state.selectedProfile.getProfilePicture()
        },
    }
}