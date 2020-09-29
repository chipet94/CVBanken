import UserFileService from "@/services/UserFileService";

const initialState = {
    files: [],
    userProgramme: {}
}

export const files = {
    namespaced: true,
    state: initialState,
    getters:{
        getAllLocal : state => {
            return state.programmes
        },
    },
    actions:{
        getAll({commit}){
            return UserFileService.getAll().then(
                files => {
                    commit('userFilesSuccess', files);
                    return Promise.resolve(files);
                },
                error => {
                   // commit('educationsFailure');
                    return Promise.reject(error);
                }

            )
        },
        getAllByUserId({commit}, userId){
            return UserFileService.getAllByUser(userId).then(
                files => {
                    commit('userFilesSuccess', files);
                    return Promise.resolve(files);
                },
                error => {
                    // commit('educationsFailure');
                    return Promise.reject(error);
                }

            )
        },
        getById({commit}, id){
            return UserFileService.getFile(id).then(
                file => {
                    commit("userFileSuccess", file)
                    return Promise.resolve(file);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        removeById({commit},id){
            return UserFileService.removeFile(id).then(
                file => {
                    commit("userFileRemovedSuccess")
                    return Promise.resolve(file);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
    },
    mutations: {
        userFilesSuccess(state, files) {
            state.files = files;
        },
        loginFailure(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
        userFileSuccess(state, file) {
            state.files.map(sFile => sFile.id === file.id
                ?{...sFile, file}
                : sFile
            );
        },
        userFileRemovedSuccess(state){
            state.files = null;
        },
        registerSuccess(state) {
            state.status.loggedIn = false;
        },
        educationsFailure(state) {
            state.status.loggedIn = false;
        }
    }
}
