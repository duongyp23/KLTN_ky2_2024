import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '@/components/layout/view/HomePage/HomePage.vue';
import LoginForm from '@/components/layout/view/Login/LoginForm.vue';
import RegisterForm from '@/components/layout/view/Login/RegisterForm.vue';
import DictionaryPage from './components/layout/view/Dictionary/DictionaryPage.vue';
import UserInfomation from './components/layout/view/Login/UserInfomation.vue';

const routes = [
    {
        path: '/:catchAll(.*)',
        component: Error
    },
    {
        path: '/dictionary',
        component: DictionaryPage,
    },
    {
        path: '/login',
        component: LoginForm
    },
    {
        path: '/register',
        component: RegisterForm
    },
    {
        path: '/userinfo',
        component: UserInfomation
    },
    {
        path: '/homepage',
        component: HomePage
    }

]


const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router