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
                    commit("userFilesSuccess", files);
                    return Promise.resolve(files);
                },
                error => {
                    return Promise.reject(error)
                }
            )
        },
        SetCV({commit}, id) {
            return UserFileService.setCv(id).then(
                files => {
                    commit("userFileSuccess", files)
                    return Promise.resolve(files)
                },
                err => {
                    return Promise.reject(err)
                }
            )
        }
    },
    mutations: {
        userFilesSuccess(state, files) {
            state.files = files;
        },
        userFileSuccess(state, file) {
            state.files.map(sFile => sFile.id === file.id
                ? {...sFile, file}
                : sFile
            );
        },
        gotData(state) {
            console.log(state.files)
        },
        userFileRemovedSuccess(state, file) {
            state.files.slice(state.files.findIndex(z => z.id === file.id), 1);
        },
    }
}
