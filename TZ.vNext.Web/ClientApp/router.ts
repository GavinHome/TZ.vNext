import Router, { RouteConfig } from 'vue-router'
import spin from './components/common/TzSpin'
import StoreCache from './components/common/TzStoreCache'
import { TzFunctionConst } from './components/common/TzCommonConst';
import { Component } from 'vue-router/types/router';

const login = require('./components/pages/login/index.vue.html')

const home = resolve => {
    spin.show()
    require(['./components/home/home.vue.html'], spin.resolve(resolve))
}

const salary_grid = resolve => {
    spin.show()
    require(['./components/pages/salary/salary.grid.vue.html'], spin.resolve(resolve))
}

const salary_create = resolve => {
    spin.show()
    require(['./components/pages/salary/salary.create.vue.html'], spin.resolve(resolve))
}

const salary_detail = resolve => {
    spin.show()
    require(['./components/pages/salary/salary.detail.vue.html'], spin.resolve(resolve))
}

const form_demo = resolve => {
    spin.show()
    require(['./components/wrapper/TzSuperForm/Dev.vue.html'], spin.resolve(resolve))
}

const form_builder = resolve => {
    spin.show()
    require(['./components/wrapper/TzSuperFormBuilder/index.vue.html'], spin.resolve(resolve))
}

const processing = resolve => {
    spin.show()
    require(['./components/pages/product/processing.vue.html'], spin.resolve(resolve))
}

const product_view = resolve => {
    spin.show()
    require(['./components/pages/product/product.detail.vue.html'], spin.resolve(resolve))
}

const employee_grid = resolve => {
    spin.show()
    require(['./components/pages/employee/employee.grid.vue.html'], spin.resolve(resolve))
}

const employee_create = resolve => {
    spin.show()
    require(['./components/pages/employee/employee.create.vue.html'], spin.resolve(resolve))
}

const employee_detail = resolve => {
    spin.show()
    require(['./components/pages/employee/employee.detail.vue.html'], spin.resolve(resolve))
}

const routes: RouteConfig[] = [
    {
        path: '*',
        redirect: '/home'
    },
    {
        path: '/login',
        name: 'login',
        component: login as Component,
        meta: {
            title: '登录'
        },
        props: (route) => ({ redirect: route.query.redirect })
    },
    {
        path: '/home',
        name: 'home',
        component: require('./components/home/index.vue.html'),
        meta: {
            title: '主页',
            requireAuth: true
        },
        children: [
            {
                path: '', name: 'dashboard', component: { template: `<router-view></router-view>` },
                meta: {
                    breadcrumb: '我的仪表盘',
                    title: '我的仪表盘',
                    icon: 'fa fa-dashboard'
                },
                children: [
                    {
                        path: '',
                        name: 'myhome',
                        component: { template: `<router-view></router-view>` },
                        meta: {
                            breadcrumb: '我的首页',
                            icon: 'fa fa-caret-right',
                            title: '我的首页'
                        },
                        children: [
                            {
                                path: '',
                                name: 'home_list',
                                component: home,
                                meta: {
                                    parent: 'myhome',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    title: '我的首页'
                                },
                            },
                        ]
                    }
                ]
            },
            {
                path: '/building', name: 'building', component: { template: '<router-view></router-view>' },
                meta: {
                    breadcrumb: '我的产品',
                    title: '我的产品',
                    icon: 'fa fa-building',
                    functionId: TzFunctionConst.PRODUCT_MANAGEMENT
                },
                children: [
                    {
                        path: '/products',
                        name: 'products',
                        component: { template: '<router-view></router-view>' },
                        meta: {
                            parent: 'building',
                            title: '产品集',
                            icon: 'fa fa-caret-right',
                            functionId: TzFunctionConst.PRODUCT_LIST
                        },
                        children: [
                            {
                                path: '',
                                name: 'products_list',
                                component: require("./components/pages/product/index.vue.html"),
                                meta: {
                                    parent: 'products',
                                    title: '产品集',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.PRODUCT_LIST
                                }
                            },
                            {
                                path: '/product/view',
                                name: 'products_view',
                                component: product_view,
                                meta: {
                                    breadcrumb: '详情',
                                    parent: 'products',
                                    title: '详情',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.PRODUCT_DETAIL
                                },
                                props: (route) => ({ id: route.query.id })
                            }
                        ]
                    },
                    {
                        path: '/shareWithMe',
                        name: 'shareWithMe',
                        component: { template: '<span>未完成</span>' },
                        meta: {
                            parent: 'building',
                            title: '分享给我',
                            icon: 'fa fa-caret-right',
                            functionId: TzFunctionConst.PRODUCT_LIST
                        },
                    },
                    {
                        path: '/collaborate',
                        name: 'collaborate',
                        component: { template: '<span>未完成</span>' },
                        meta: {
                            parent: 'building',
                            title: '与我协作',
                            icon: 'fa fa-caret-right',
                            functionId: TzFunctionConst.PRODUCT_LIST
                        },
                    },
                    {
                        path: '/open',
                        name: 'open',
                        component: { template: '<span>未完成</span>' },
                        meta: {
                            parent: 'building',
                            title: '公开作品',
                            icon: 'fa fa-caret-right',
                            functionId: TzFunctionConst.PRODUCT_LIST
                        },
                    },
                    {
                        path: '/trash',
                        name: 'trash',
                        component: { template: '<router-view></router-view>' },
                        meta: {
                            parent: 'building',
                            title: '回收站',
                            icon: 'fa fa-caret-right',
                        },
                        children: [
                            {
                                path: '',
                                name: 'trash_list',
                                component: { template: "<span>未完成</span>" },
                                meta: {
                                    parent: 'trash',
                                    title: '回收站',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.PRODUCT_LIST
                                }
                            }
                        ]
                    }
                ]
            },
            {
                path: '/employees', name: 'employees', component: { template: `<router-view></router-view>` },
                meta: {
                    breadcrumb: '人力资源',
                    title: '人力资源',
                    icon: 'fa fa-money',
                    functionId: TzFunctionConst.Employee_MANAGEMENT
                },
                children: [
                    {
                        path: '/employee',
                        name: 'employee',
                        component: { template: `<router-view></router-view>` },
                        meta: {
                            breadcrumb: '用户管理',
                            title: '用户管理',
                            icon: 'fa fa-caret-right',
                            functionId: TzFunctionConst.Employee_LIST
                        },
                        children: [
                            {
                                path: '',
                                name: 'employee_grid',
                                component: employee_grid,
                                meta: {
                                    breadcrumb: '用户列表',
                                    parent: 'employee',
                                    title: '用户列表',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.Employee_LIST
                                }
                            },
                            {
                                path: 'create',
                                name: 'employee_create',
                                component: employee_create,
                                meta: {
                                    breadcrumb: '新增用户',
                                    parent: 'employee',
                                    title: '新增用户',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.Employee_CREATE
                                },
                                props: (route) => ({ id: route.query.id })
                            },
                            {
                                path: 'edit',
                                name: 'employee_edit',
                                component: employee_create,
                                meta: {
                                    breadcrumb: '编辑用户',
                                    parent: 'employee',
                                    title: '编辑用户',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.Employee_EDIT
                                },
                                props: (route) => ({ id: route.query.id })
                            },
                            {
                                path: 'detail',
                                name: 'employee_detail',
                                component: employee_detail,
                                meta: {
                                    breadcrumb: '用户详情',
                                    parent: 'employee',
                                    title: '用户详情',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.Employee_DETAIL
                                },
                                props: (route) => ({ id: route.query.id })
                            }
                        ]
                    },
                ]
            },
            {
                path: '/salary', name: 'salary', component: { template: `<router-view></router-view>` },
                meta: {
                    breadcrumb: '薪酬管理',
                    title: '薪酬管理',
                    icon: 'fa fa-money',
                    functionId: TzFunctionConst.SALARY_MANAGEMENT
                },
                children: [
                    {
                        path: '/basic',
                        name: 'basic',
                        component: { template: `<router-view></router-view>` },
                        meta: {
                            breadcrumb: '基础数据',
                            title: '基础数据',
                            icon: 'fa fa-caret-right',
                            functionId: TzFunctionConst.SALARY_BASIC
                        },
                        children: [
                            {
                                path: '/parts',
                                name: 'parts',
                                component: { template: `<router-view></router-view>` },
                                meta: {
                                    breadcrumb: '薪酬项',
                                    title: '薪酬项',
                                    icon: 'fa fa-caret-right',
                                    functionId: TzFunctionConst.SALARY_BASIC_SALARY_LIST
                                },
                                children: [
                                    {
                                        path: '',
                                        name: 'salary_parts_grid',
                                        component: salary_grid,
                                        meta: {
                                            parent: 'parts',
                                            title: '薪酬项',
                                            icon: 'fa fa-caret-right',
                                            isHidden: true,
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_LIST
                                        }
                                    },
                                    {
                                        path: 'create',
                                        name: 'salary_parts_create',
                                        component: salary_create,
                                        meta: {
                                            breadcrumb: '新增薪酬项',
                                            parent: 'parts',
                                            title: '新增薪酬项',
                                            icon: 'fa fa-caret-right',
                                            isHidden: true,
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_CREATE
                                        },
                                        props: (route) => ({ id: route.query.id })
                                    },
                                    {
                                        path: 'edit',
                                        name: 'salary_parts_edit',
                                        component: salary_create,
                                        meta: {
                                            breadcrumb: '编辑薪酬项',
                                            parent: 'parts',
                                            title: '编辑薪酬项',
                                            icon: 'fa fa-caret-right',
                                            isHidden: true,
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_EDIT
                                        },
                                        props: (route) => ({ id: route.query.id })
                                    },
                                    {
                                        path: 'detail',
                                        name: 'salary_parts_detail',
                                        component: salary_detail,
                                        meta: {
                                            breadcrumb: '薪酬项详情',
                                            parent: 'parts',
                                            title: '薪酬项详情',
                                            icon: 'fa fa-caret-right',
                                            isHidden: true,
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_DETAIL
                                        },
                                        props: (route) => ({ id: route.query.id })
                                    }
                                ]
                            },
                        ]
                    }
                ]
            },
            {
                path: '/demo', name: 'demo', component: { template: `<router-view></router-view>` },
                meta: {
                    breadcrumb: '案例',
                    title: '案例',
                    icon: 'fa fa-suitcase'
                },
                children: [
                    {
                        path: '/super_form',
                        name: 'super_form',
                        component: form_demo,
                        meta: {
                            title: '表单',
                            breadcrumb: '表单',
                            parent: 'demo',
                            icon: 'fa fa-caret-right',
                        },
                        children: [
                            {
                                path: '',
                                name: 'super_form_page',
                                component: { template: "<span>未完成</span>" },
                                meta: {
                                    parent: 'super_form',
                                    title: '表单',
                                    icon: 'fa fa-caret-right',
                                    isHidden: true,
                                    functionId: TzFunctionConst.SALARY_BASIC_SALARY_LIST
                                }
                            }
                        ]
                    },
                ]
            },
        ]
    },
    {
        path: '/form',
        name: 'form',
        component: form_builder,
        meta: {
            title: '表单设计器',
            breadcrumb: '表单生成器',
            icon: 'fa fa-building',
            isHidden: true,
        }
    },
    {
        path: '/processing',
        name: 'processing',
        component: processing,
        meta: {
            title: '设计器',
            breadcrumb: '生成器',
            icon: 'fa fa-building',
            isHidden: true,
        },
        props: (route) => ({ id: route.query.id })
    }
]

var router = new Router({
    mode: 'history',
    routes: routes
})

router.beforeEach((to, from, next) => {
    document.title = to.meta.title + " - 业务表单设计系统"
    let cache = new StoreCache('auth')
    if (cache.get('expirationTime')) {
        let isExpire = new Date(cache.get('expirationTime')).getTime() < (new Date()).getTime()
        if (isExpire) {
            cache.clear()
        }
    } else {
        cache.clear()
    }

    let token = cache.get('token')
    let requireAuth = to.matched.some(r => r.meta.requireAuth)
    if (requireAuth && !token) {
        next({
            name: 'login',
            query: { redirect: to.fullPath }
        })
    } else {
        if (token && to.name === 'login') {
            next({ name: 'home' })
        } else {
            if (to.redirectedFrom && from.meta.parent && to.redirectedFrom.toLowerCase().indexOf(from.meta.parent.toLowerCase()) > 0) {
                next(to.redirectedFrom.toLowerCase().replace(from.meta.parent.toLowerCase(), '').replace('//', '/'))
                return
            }

            next()
        }
    }
})
export default router
