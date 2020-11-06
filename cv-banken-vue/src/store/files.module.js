import UserFileService from "@/services/UserFileService";

const initialState = {
    files: [],
}

export const files = {
    namespaced: true,
    state: initialState,
    getters: {},
    actions: {
        getAll({commit}) {
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
        getAllByUserId({commit}, userId) {
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
        getById({commit}, id) {
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
        downloadById({commit}, id) {
            return UserFileService.downloadFile(id).then(
                file => {
                    commit("gotData")
                    return Promise.resolve(file)
                },
                error => {
                    return Promise.reject(error)
                },
            )
        },
        downloadCv({commit}, id) {
            return UserFileService.downloadCv(id).then(
                file => {
                    commit("gotData")
                    return Promise.resolve(file)
                },
                error => {
                    return Promise.reject(error)
                },
            )
        },
        removeById({commit}, id) {
            return UserFileService.removeFile(id).then(
                file => {
                    commit("userFileRemovedSuccess", file)
                    return Promise.resolve(file);
                },
                error => {
                    return Promise.reject(error);
                }
            )
        },
        UploadFiles({commit}, files) {
            return UserFileService.uploadFile(files).then(
                files => {
                    commit("userFilesSuccess");
                    return Promise.resolve(files);
                },
                error => {
                    return Promise.reject(error)
                }
            )
        },
        SetCV({commit}, cv) {
            return UserFileService.uploadCv(cv).then(
                files => {
                    commit("userFileSuccess", files)
                    return Promise.resolve(files)
                },
                err => {
                    return Promise.reject(err)
                }
            )
        },
        removeCv({commit}, id) {
            return UserFileService.removeCv(id).then(
                file => {
                    commit("userFileRemovedSuccess", file)
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
        userFileSuccess() {
            console.log("uploaded...")
        },
        gotData(state) {
            console.log(state.files)
        },
        userFileRemovedSuccess() {
            // state.files.slice(state.files.findIndex(z => z.id === id), 1);
        },
    }
}
