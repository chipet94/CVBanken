export default class User {
    email;
    firstName;
    lastName;
    role;
    programmeId;
    token;
    id;
    constructor(data = {}) {
        this.id = data.id;
        this.firstName = data.firstName;
        this.lastName = data.lastName;
        this.email = data.email;
        this.role = data.role;
        this.programmeId = data.programmeId;
        this.token = data.token;
    }

    static FromData(data) {
        if (data != null) {
            return new User(data)
        }
        return User.EmptyUser();
    }

    static EmptyUser(){
        return new User()
    }

}
