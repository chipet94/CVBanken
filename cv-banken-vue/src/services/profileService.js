import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL
export class ProfileService {

    getAllProfiles(){
        return axios.get(API_URL + "profile/all", {
        })
    }
    getProfile(url = ""){
        return axios.get(API_URL + "profile/url/" + url, {
        })
    }
    getCurrentUserProfile(){
        return axios.get(API_URL + "profile/", {
            headers: authHeader(),
            credentials: 'include'
        })
    }
    getProfilePicture(id = 0){
        return axios.get(API_URL + "profile/"+ id +"/picture" , {responseType: 'blob'}).then( res => {
            return res;
        })
    }
    getUserById(id){
        return axios.get(API_URL + "user/" + id).then(res => {
            return res.data;
        })
    }
    updateProfile(id, updateModel){
        console.log(id, updateModel)
        return axios.put(API_URL + "profile/" + id, updateModel,
            {headers:authHeader()})
    }
    updateProfilePicture(id, updateModel){
        console.log(id, updateModel)
        return axios.put(API_URL + "profile/" + id + "/picture", updateModel, {headers:authHeader()})
    }
}
export default new ProfileService();
