import UserFileService from "@/services/UserFileService";
import EducationService from "@/services/EducationService";



export default class User {
    email;
    firstName;
    lastName;
    programmeId;
    id;
    profile;
    files = [];
    programme = {};
    url;
    private;
    searching;
    class_name;
    description;
    profilePicture;
    gotCv;

    constructor(data = {}) {
        this.id = data.id,
            this.firstName = data.firstName;
        this.lastName = data.lastName;
        this.email = data.email;
        this.programmeId = data.programmeId;
        this.class_name = data.programme;
        this.categoryName = data.category;
        this.url = data.url;
        this.searching = data.searching;
        this.private = data.private;
        this.gotCv = data.gotCv;
        this.description = data.description;
        this.profilePictureId = data.profilePictureId ?? '';
        this.profilePicture = data.profilePicture;
        this.files = data.files;
    }

    static FromData(data) {
        if (data != null) {
            return new User(data)
        }
        return User.EmptyUser();
    }

    static EmptyUser() {
        return new User()
    }

    async getProgramme() {
        return await EducationService.getEducation(this.programmeId).then(res => {
            this.programme = res;
            console.log(res)
        });
    }
    dataToProfilePicture(data){
        if (data){
            let reader = new FileReader();
            //reader.readAsDataURL(res.data)
            reader.readAsDataURL(data);
            reader.onload = () => {
                return reader.result;
            }
        }
        else
            return null;

    }
    async setProfilePicture(picdata){
        if (picdata){
            let reader = new FileReader();
            //reader.readAsDataURL(res.data)
            reader.readAsDataURL(picdata);
            reader.onload = () => {
                this.profilePicture = reader.result;
            }
        }
    }
    async getProfilePicture() {
        // await this.$store.dispatch("profile/getProfilePicture", this.id).then(res => {
        //     console.log(res)
        //     if (res.status === 200) {
        //         let reader = new FileReader();
        //         //reader.readAsDataURL(res.data)
        //         reader.readAsDataURL(res.data);
        //         reader.onload = () => {
        //             this.profilePicture = reader.result;
        //         }
        //     }
            // this.profilePicture =  'data:image/jpg;base64,'.concat(this.profilePicture.concat(res.data));

        // }).catch(err => {
        //     console.log(err)
        //     this.profilePicture = null
        // })
    }

    async getFiles() {
        return await UserFileService.getAllByUser(this.id).then(res => {
            this.files = res;
        })
    }

}
