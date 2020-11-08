import SiteService from "@/services/SiteService";
import {HomeInfo} from "@/models/HomeInfo";

const initialState = {
    HomeData: HomeInfo.prototype,
    messages: []
}

export const Site = {
    namespaced: true,
    state: initialState,
    getters: {
        getHome: state => {
            return state.HomeData;
        },
        getMessages: state => {
            return state.messages;
        },
        getMessage: state => (index) => {
            return state.messages[index]
        },

    },
    actions: {
        getHome({commit}) {
            return SiteService.getHome().then(
                home => {
                    commit("HomeSuccess", HomeInfo.FromData(home))
                    return Promise.resolve(HomeInfo.FromData(home))
                },
                err => {
                    return Promise.reject(err);
                }
            )
        },
        getMessages({commit}) {
            return SiteService.getMessages().then(
                messages => {
                    commit("messagesSuccess", messages)
                    return Promise.resolve(messages)
                },
                err => {
                    return Promise.reject(err);
                }
            )
        },
        addMessage({commit}, message) {
            console.log("store", message)
            return SiteService.addMessage(message).then(
                message => {
                    commit("addedMessageSuccess")
                    return Promise.resolve(message)
                },
                err => {
                    return Promise.reject(err);
                }
            )
        },
        updateMessage({commit}, {id, message}) {
            return SiteService.putMessage(id, message).then(
                message => {
                    commit("updatedMessageSuccess", message)
                    return Promise.resolve(message)
                },
                err => {
                    return Promise.reject(err);
                }
            )
        },
        deleteMessage({commit}, id) {
            return SiteService.deletedMessage(id).then(
                message => {
                    commit("deletedMessageSuccess", id)
                    return Promise.resolve(message)
                },
                err => {
                    return Promise.reject(err);
                }
            )
        },
    },
    mutations: {
        HomeSuccess(state, home) {
            console.log(home)
            state.HomeData = home;
        },
        messagesSuccess(state, messages){
            state.messages = messages;
        },
        addedMessageSuccess(){
            console.log("Message added..")
            // let target = state.messages.find(cat => {
            //     return message.id === cat.id;
            // });
            // target ? Object.assign(target, message) : state.categories.push(message);
        },
        updatedMessageSuccess(state, {id, message}){
            message.id = id;
            let target = state.messages.find(cat => {
                return id === cat.id;
            });
            target ? Object.assign(target, message) : state.categories.push(message);
        },
        deletedMessageSuccess(state, id){
            state.messages.pop(p => p.id === id)
        }
    }
}