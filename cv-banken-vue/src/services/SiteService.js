import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL

class SiteService {
    getHome() {
        return axios
            .get(API_URL + 'site/')
            .then(response => {
                return response.data;
            });
    }
    getMessages() {
        return axios
            .get(API_URL + 'site/messages')
            .then(response => {
                console.log(response)
                return response.data;
            });
    }
    addMessage(messageRequest) {
        console.log("service", messageRequest)
        return axios
            .post(API_URL + 'site/messages', messageRequest, {
                headers: authHeader()
            })
            .then(response => {
                return response.data;
            });
    }
    putMessage(id,message) {
        return axios
            .put(API_URL + 'site/messages/' + id, message, {
                headers: authHeader()
            })
            .then(response => {
                return response.data;
            });
    }
    deletedMessage(id) {
        return axios
            .delete(API_URL + 'site/messages/'+id , {
                headers: authHeader()
            })
            .then(response => {
                return response.data;
            });
    }

}

export default new SiteService();