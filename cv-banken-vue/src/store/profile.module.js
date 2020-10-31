import profileService from "@/services/profileService";

import User from "@/models/User";
const initialState = {
    selectedProfile: User.prototype,
    userProfile: {}
}

export const profile = {
    namespaced: true,
    state: initialState,
    getters:{
        getProfile : state => {
            return state.selectedProfile;
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
                    return Promise.resolve(profile.data);
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
                    dispatch("getUserProfile").then(commit("profileUpdated"))
                    //commit("profileUpdated");
                    return Promise.resolve(profile.data);
                },
                error => {
                    commit("profileUpdateFail")
                    return Promise.reject(error)
                }
            )
        },
        updateProfilePicture({commit, state}, request) {
            return profileService.updateProfilePicture(state.selectedProfile.id, request).then(
                res => {
                    commit("profilePictureUpdated");
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
            state.selectedProfile = new User(profile)
            await state.selectedProfile.getProfilePicture();
        },
        userProfileFailure(state) {
            state.selectedProfile = null;
        },
        profileFetchSuccess(state, profile) {
            state.selectedProfile = new User(profile)
        },
        profileUpdated(){

            },
        profileUpdateFail(state, profile){

            state.selectedProfile = {...state.selectedProfile, profile}
        },
        async profilePictureUpdated(state){
            console.log(state)
            await state.selectedProfile.getProfilePicture()
        },
    }
}