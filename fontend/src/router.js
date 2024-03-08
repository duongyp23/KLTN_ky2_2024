import { createRouter, createWebHistory } from 'vue-router'
import MsTable from '@/components/layout/view/ListProperty/MsTable.vue';
import MsLogin from '@/components/layout/view/Login/MsLogin.vue';
import MsTableVoucher from "@/components/layout/view/ListVoucher/MsTableVoucher.vue"
import MsFormVoucher from '@/components/layout/view/ListVoucher/MsFormVoucher.vue'
import MsFormFix from '@/components/layout/view/ListVoucher/MsFormFix.vue'
import MsFormSelect from '@/components/layout/view/ListVoucher/MsFormSelect.vue'

const routes = [
    {
        path: '/:catchAll(.*)',
        component: Error
    },
    {
        path: '/taisan',
        component: MsTable
    },
   {
        path:'/login',
        component: MsLogin
   },
    {
        path: '/taisan/ghitang',
        component: MsTableVoucher,
        children: [
            {
                path:'form/:id',
                component: MsFormVoucher,
                children: [
                    {
                        path: 'fixBudget',
                        component:MsFormFix
                    },
                    {
                        path: 'selectProperty',
                        component: MsFormSelect
                    }
                ]
            },
            {
                path:'newForm',
                component: MsFormVoucher,
            }
        ]

    }
    
]


const router = createRouter({
    history: createWebHistory(),
    routes: routes
})

export default router