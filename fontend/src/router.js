import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '@/components/layout/view/HomePage/HomePage.vue';
import LoginForm from '@/components/layout/view/Login/LoginForm.vue';
import RegisterForm from '@/components/layout/view/Login/RegisterForm.vue';
import DictionaryPage from './components/layout/view/Dictionary/DictionaryPage.vue';
import UserInfomation from './components/layout/view/Login/UserInfomation.vue';
import OrderDetail from './components/layout/view/Order/OrderDetail.vue';
import OrderList from './components/layout/view/OrderList/OrderList.vue';
import DashboardView from './components/layout/view/Dashboard/DashboardView.vue';

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
    },
    {
        path: '/order/:id',
        component: OrderDetail
    },
    {
        path: '/orderlist',
        component: OrderList
    },
    {
        path: '/dashboard',
        component: DashboardView
    }

]


const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router