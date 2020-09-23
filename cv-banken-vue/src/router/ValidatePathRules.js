import store from '@/store'
export const ValidatePathRules = (to, from, next) => {
    let user = store.getters["auth/getUser"]
    if (to.matched.some(record => record.meta.requireAuth)) // om sidan kräver auth.
        if (!user.token)                   //om ingen userData finns, skicka till lgoin.
            return next({
                path: '/login',
                params: {nextUrl: to.fullPath}
            })
        else if (to.matched.some(record => record.meta.requireAdmin)) // om sidan kräver admin      
            if (user.role === "Admin")
                return next()
            else
                return next(from)   //stanna om du inte är admin. 
        else
            return next()
    else if (to.matched.some(record => record.meta.guest))   // om sidan är för gäster
        if (!user.token)           // ingen userdata, gå vidare
            next()
        else
            return next({name: 'UserProfile'})     //annars gå till profil.

    //inga krav
    return next()
}