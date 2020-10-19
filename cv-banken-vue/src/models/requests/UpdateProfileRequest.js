export const updateProfileModel = request => {
    return {
        user: request.user,
        //     {
        //     firstName: request.firstName,
        //     lastName: request.lastName,
        //     programmeId: request.programmeId,
        //     email: request.email,
        //     password: request.password,
        // },
        private: request.private,
        searching: request.searching,
        description: request.description,
    }

}
