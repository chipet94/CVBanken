<<<<<<< HEAD
import UserFileService from "@/services/U   serFileService";

const initialState = {
    files: [],
=======
import UserFileService from "@/services/UserFileService";

const initialState = {
    files: [],
    userProgramme: {}
>>>>>>> File-Upload-Download
}

export const files = {
    namespaced: true,
    state: initialState,
    getters:{
<<<<<<< HEAD
=======
        getAllLocal : state => {
            return state.programmes
        },
>>>>>>> File-Upload-Download
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
<<<<<<< HEAD
        downloadById( {commit}, id){
          return UserFileService.downloadFile(id).then(
              file=> {
                  commit("gotData")
                  return Promise.resolve(file)},
              error => {return Promise.reject(error)},
              )
        },
        removeById({commit},id){
            return UserFileService.removeFile(id).then(
                file => {
                    commit("userFileRemovedSuccess", file)
=======
        removeById({commit},id){
            return UserFileService.removeFile(id).then(
                file => {
                    commit("userFileRemovedSuccess")
>>>>>>> File-Upload-Download
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
<<<<<<< HEAD
=======
        loginFailure(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
>>>>>>> File-Upload-Download
        userFileSuccess(state, file) {
            state.files.map(sFile => sFile.id === file.id
                ?{...sFile, file}
                : sFile
            );
        },
<<<<<<< HEAD
        gotData(state){
            console.log(state.files)
        },
        userFileRemovedSuccess(state, file){
            state.files.slice(state.files.findIndex(z => z.id === file.id), 1);
        },
=======
        userFileRemovedSuccess(state){
            state.files = null;
        },
        registerSuccess(state) {
            state.status.loggedIn = false;
        },
        educationsFailure(state) {
            state.status.loggedIn = false;
        }
>>>>>>> File-Upload-Download
    }
}
