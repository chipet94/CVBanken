import axios from "axios";
import {authHeader} from "@/services/AuthHeader";

const API_URL = process.env.VUE_APP_API_URL
class UserService {
    getAll() {
        return axios
            .get(API_URL + 'user',
                authHeader()
            )
            .then(response => {
                return response.data;
            });
    }
    getAllByUser(userId) {
        return axios
            .get(API_URL + 'files/user/' + userId,
                authHeader()
            )
            .then(response => {
                return response.data;
            });
    }

    getFile(id) {

        return axios.get(API_URL + "files/" + id,
            authHeader()
        ).then(res => {
            return res.data
        });
    }
    uploadFiles(files, onUploadProgress){
        let formData = new FormData();
        formData.append("files", files);

        return axios.post(API_URL + "files/upload_files", formData, {
            headers:authHeader("multipart/form-data"),
            credentials: 'include',
            onUploadProgress
        }).then(res => {
            return res.data;
        })
    }
    downloadFile(id = 0){
        return axios.get(API_URL + "files/download/" + id, {
            headers: authHeader(),
            responseType: 'blob',
        }).then(res => {
            return res.data;
        })
    }


    uploadFile(uploadfile = FormData){
        // let formData = new FormData();
        // formData.append("files", files);
        console.log("Toupload",uploadfile)
        return axios.post(API_URL + "files/upload", uploadfile, {
            headers:authHeader("multipart/form-data"),
        }).then(res => {
            return res.data;
        })
    }
    removeFile(id){
        return axios.delete(API_URL + "files/" + id, {
            headers:authHeader(),
            credentials: 'include'
        }).then(res => {
            return res;
        })
    }
}

export default new UserService();