import Vue from 'vue'
import Router from 'vue-router'
import Login from '../views/Login.vue'
import Profile from '../views/Profile'
import UserProfile from '../views/UserProfile'
import AdminPage from "@/views/AdminPage";
import Home from "@/views/Home";
import {ValidatePathRules} from "@/router/ValidatePathRules";


Vue.use(Router)

let router = new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            name: 'Home',
            component: Home,
            meta: {
                guest: true
            }
        },
        {
            path: '/error',
            name: 'Error',
            meta: {
                guest: true
            },
            component: () => import('@/views/ErrorPage'),
        },
        {
            path: '/unauthenticated',
            name: 'Unauthenticated',
            meta: {
                guest: true
            },
            component: () => import('@/views/NotAuthenticatedPage'),
        },
        {
            path: '/login',
            name: 'Login',
            component: Login,
            meta: {
                guest: true
            }
        },
        {
            path: '/sign-up',
            name: 'SignUp',
            meta: {
                guest: true
            },
            component: () => import('@/views/Signup.vue'),

        },
        {
            path: '/my_profile',
            name: 'UserProfile',
            meta: {
                requireAuth: true,
            },
            component: Profile
        },
        {
            path: '/educations',
            name: 'Educations',
            meta: {
                guest: true
            },
            component: () => import('@/views/EducationsList.vue')

        },
        {
            path: '/utbildningar',
            name: 'Utbildningar',
            meta: {
                guest: true
            },
            component: () => import('@/views/CategoryView.vue')

        },
        {
            path: '/utbildningar/:name',
            name: 'Kategori',
            meta: {
                guest: true
            },
            component: () => import('@/views/CategoryPage.vue')

        },
        {
            path: '/klass/:name',
            name: 'Klass',
            meta: {
                guest: true
            },
            component: () => import('@/views/ProgrammePage.vue')

        },
        {
            path: '/profile/:url',
            name: 'Profile',
            component: UserProfile,
            meta: {
                guest: true
            },
        },
        {
            path: '/admin_dashboard',
            name: 'AdminDashboard',
            meta: {
                requireAuth: true,
                requireAdmin: true
            },
            component: AdminPage
        },
        {
            path: '*',
            component: Login
        }
    ]
});

router.beforeEach((to, from, next) => ValidatePathRules(to, from, next))

export default router

