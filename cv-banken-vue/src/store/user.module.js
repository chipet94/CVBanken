import UserService from "@/services/UserService";
import User from "@/models/User";

const initialState = {
    publicUsers: [],
    allUsers: [],
    currentUsers: [],
    selectedUser: User.prototype,
}

export const user = {
    namespaced: true,
    state: initialState,
    getters: {
        admin_GetAllUsers: state => {
            return state.allUsers;
        },
        AllPublicUsers: state => {
            return state.publicUsers;
        },
        AllCurrentUsers: state => {
            return state.currentUsers;
        }
    },
    actions: {
        all({commit}) {
            return UserService.getAll().then(
                users => {
                    commit("usersSuccess", users.data)
                    return Promise.resolve(users.data)
                },
                err => {
                    console.log(err);
                    return Promise.reject(err)
                }
            )
        },
        admin_all({commit}) {
            return UserService.adminGetAll().then(
                users => {
                    commit("adminUsersSuccess", users.data)
                    return Promise.resolve(users.data)
                },
                err => {
                    console.log(err);
                    return Promise.reject(err)
                }
            )
        },
        allInProgramme({commit}, id) {
            return UserService.getAllinProgramme(id).then(
                users => {
                    commit("currentUsersSuccess", users.data)
                    return Promise.resolve(users.data)
                },
                err => {
                    console.log(err);
                    return Promise.reject(err)
                }
            )
        },
        allInCategory({commit}, id) {
            return UserService.getAllinCategory(id).then(
                users => {
                    commit("currentUsersSuccess", users.data)
                    return Promise.resolve(users.data)
                },
                err => {
                    console.log(err);
                    return Promise.reject(err)
                }
            )
        },

    },
    mutations: {
        adminUsersSuccess(state, users) {
            state.allUsers = users
        },
        usersSuccess(state, users) {
            state.publicUsers = users
        },
        currentUsersSuccess(state, users) {
            state.currentUsers = users
        },


    }
}