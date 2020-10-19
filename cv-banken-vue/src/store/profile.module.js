import profileService from "@/services/profileService";
import {Profile} from "@/models/Profile";
const initialState = {
    visitedProfiles:[],
    userProfile: Profile.prototype
}

export const profile = {
    namespaced: true,
    state: initialState,
    getters:{
        getProfile : state => {
            return state.userProfile;
        },
    },
    actions:{
        getAllProfiles(){
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
        getUserProfile({commit}){
            return profileService.getCurrentUserProfile().then(
                profile => {
                    commit('userProfileSuccess', profile.data);
                    console.log("from store:",profile)
                    return Promise.resolve(new Profile(profile.data));
                },
                error => {
                    commit('educationsFailure');
                    return Promise.reject(error);
                }

            )
        },
        getByUrl({commit}, url){
            return profileService.getProfile(url).then(
                profile => {
                    commit("profileFetchSuccess", profile.data)
                    return Promise.resolve(new Profile(profile.data));
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        updateProfile({commit, state}, request) {
            return profileService.updateProfile(state.userProfile.profileId, request).then(
                profile => {
                    commit("profileUpdated", profile.data);
                    return Promise.resolve(profile.data);
                },
                error => {
                    commit("profileUpdateFail")
                    return Promise.reject(error)
                }
            )
        },
        updateProfilePicture({commit, state}, request) {
            return profileService.updateProfilePicture(state.userProfile.profileId, request).then(
                res => {
                    commit("profilePictureUpdated");
                    return Promise.resolve(res);
                },
                error => {
                    commit("profileUpdateFail")
                    return Promise.reject(error)
                }
            )
        }
    },
    mutations: {
        async userProfileSuccess(state, profile) {
            state.userProfile = new Profile( profile);
            await state.userProfile.getProfilePicture();
        },
        userProfileFailure(state) {
            state.userProfile = null;
        },
        profileFetchSuccess(state, profile) {
            state.visitedProfiles.map(prof => prof.profileId === profile.profileId
                ?{...prof, profile}
                : prof
            );
        },
        async profileUpdated(state, profile){
            console.log("old", state.userProfile)
            console.log("to be applyed: ", profile)
            state.userProfile = new Profile( profile);
            await state.userProfile.getProfilePicture();
            console.log("new",state.userProfile)
        },
        profileUpdateFail(state, profile){
            state.userProfile = {...state.userProfile, profile}
        },
        async profilePictureUpdated(state){
            console.log(state)
            await state.userProfile.getProfilePicture()
        },

        registerSuccess(state) {
            state.status.loggedIn = false;
        },
        educationsFailure(state) {
            state.status.loggedIn = false;
        }
    }
}