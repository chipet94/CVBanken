import SiteService from "@/services/SiteService";
import {HomeInfo} from "@/models/HomeInfo";
const initialState = {
    HomeData: HomeInfo.prototype,
}

export const Site = {
    namespaced: true,
    state: initialState,
    getters:{
        getHome : state => {
            return state.HomeData;
        }
    },
    actions:{
        getHome({commit}){
            return SiteService.getHome().then(
                home => {
                    commit("HomeSuccess", HomeInfo.FromData(home))
                    return Promise.resolve(HomeInfo.FromData(home))
                },
                err => {
                  return Promise.reject(err);
                }
            )
        }
    },
    mutations:{
        HomeSuccess(state, home){
            console.log(home)
            state.HomeData = home;
        }
    }
}