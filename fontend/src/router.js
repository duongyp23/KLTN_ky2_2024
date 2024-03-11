import { createRouter, createWebHistory } from 'vue-router'
import ProductList from '@/components/layout/view/ListProduct/MsTable.vue';
import CategoryList from '@/components/layout/view/ListCategory/CategoryTable.vue';
import Login from '@/components/layout/view/Login/MsLogin.vue';

const routes = [
    {
        path: '/:catchAll(.*)',
        component: Error
    },
    {
        path: '/dictionary/product',
        component: ProductList
    },
    {
        path: '/dictionary/label',
        component: CategoryList
    },
    {
        path: '/login',
        component: Login
    }

]


const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router