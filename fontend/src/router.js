import { createRouter, createWebHistory } from 'vue-router'
import ProductList from '@/components/layout/view/ListProduct/ProductList.vue';
import CategoryList from '@/components/layout/view/ListCategory/CategoryTable.vue';
import HomePage from '@/components/layout/view/HomePage/HomePage.vue';
import LoginForm from '@/components/layout/view/Login/LoginForm.vue';
import RegisterForm from '@/components/layout/view/Login/RegisterForm.vue';
import DictionaryPage from './components/layout/view/Dictionary/DictionaryPage.vue';

const routes = [
    {
        path: '/:catchAll(.*)',
        component: Error
    },
    {
        path: '/dictionary',
        component: DictionaryPage,
        children: [
            {
                path: '/dictionary/product',
                component: ProductList
            },
            {
                path: '/dictionary/category',
                component: CategoryList
            }
        ]
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
        path: '/homepage',
        component: HomePage
    }

]


const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router