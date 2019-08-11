import Router from 'vue-router'
import spin from './components/common/TzSpin'
import StoreCache from './components/common/TzStoreCache'
import { TzFunctionConst } from './components/common/TzCommonConst';

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

const routes = [
    {
        path: '*',
        redirect: '/home'
    },
    {
        path: '/login',
        name: 'login',
        title: '登录',
        component: login,
        meta: {
            title: '登录'
        },
        props: (route) => ({ redirect: route.query.redirect })
    },
    {
        path: '/home',
        name: 'home',
        title: '主页',
        component: require('./components/home/index.vue.html'),
        meta: {
            requireAuth: true
        },
        children: [
            {
                path: '', name: 'dashboard', title: '我的仪表盘', icon: 'fa fa-dashboard', component: { template: `<router-view></router-view>` },
                meta: {
                    breadcrumb: '我的仪表盘',
                    title: '我的仪表盘'
                },
                children: [
                    {
                        path: '',
                        name: 'myhome',
                        title: '我的首页',
                        icon: 'fa fa-caret-right',
                        component: { template: `<router-view></router-view>` },
                        meta: {
                            breadcrumb: '我的首页',
                            title: '我的首页'
                        },
                        children: [
                            {
                                path: '',
                                name: 'home_list',
                                title: '我的首页',
                                icon: 'fa fa-caret-right',
                                component: home,
                                isHidden: true,
                                meta: {
                                    parent: 'myhome',
                                    title: '我的首页'
                                },
                            },
                        ]
                    }
                ]
            },
            {
                path: '/salary', name: 'salary', title: '薪酬管理', icon: 'fa fa-money', component: { template: `<router-view></router-view>` },
                meta: {
                    breadcrumb: '薪酬管理',
                    title: '薪酬管理',
                    functionId: TzFunctionConst.SALARY_MANAGEMENT
                },
                children: [
                    {
                        path: '/basic',
                        name: 'basic',
                        title: '基础数据',
                        icon: 'fa fa-caret-right',
                        component: { template: `<router-view></router-view>` },
                        meta: {
                            breadcrumb: '基础数据',
                            title: '基础数据',
                            functionId: TzFunctionConst.SALARY_BASIC
                        },
                        children: [
                            {
                                path: '/parts',
                                name: 'parts',
                                title: '薪酬项',
                                icon: 'fa fa-caret-right',
                                component: { template: `<router-view></router-view>` },
                                meta: {
                                    breadcrumb: '薪酬项',
                                    title: '薪酬项',
                                    functionId: TzFunctionConst.SALARY_BASIC_SALARY_LIST
                                },
                                children: [
                                    {
                                        path: '',
                                        name: 'salary_parts_grid',
                                        title: '列表',
                                        icon: 'fa fa-caret-right',
                                        isHidden: true,
                                        component: salary_grid,
                                        meta: {
                                            parent: 'parts',
                                            title: '薪酬项',
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_LIST
                                        }
                                    },
                                    {
                                        path: 'create',
                                        name: 'salary_parts_create',
                                        title: '新增薪酬项',
                                        icon: 'fa fa-caret-right',
                                        isHidden: true,
                                        component: salary_create,
                                        meta: {
                                            breadcrumb: '新增薪酬项',
                                            parent: 'parts',
                                            title: '新增薪酬项',
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_CREATE
                                        },
                                        props: (route) => ({ id: route.query.id })
                                    },
                                    {
                                        path: 'edit',
                                        name: 'salary_parts_edit',
                                        title: '编辑薪酬项',
                                        icon: 'fa fa-caret-right',
                                        isHidden: true,
                                        component: salary_create,
                                        meta: {
                                            breadcrumb: '编辑薪酬项',
                                            parent: 'parts',
                                            title: '编辑薪酬项',
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_EDIT
                                        },
                                        props: (route) => ({ id: route.query.id })
                                    },
                                    {
                                        path: 'detail',
                                        name: 'salary_parts_detail',
                                        title: '薪酬项详情',
                                        icon: 'fa fa-caret-right',
                                        isHidden: true,
                                        component: salary_detail,
                                        meta: {
                                            breadcrumb: '薪酬项详情',
                                            parent: 'parts',
                                            title: '薪酬项详情',
                                            functionId: TzFunctionConst.SALARY_BASIC_SALARY_DETAIL
                                        },
                                        props: (route) => ({ id: route.query.id })
                                    }
                                ]
                            },
                        ]
                    }
                ]
            }
        ]
    }
]

var router = new Router({
    mode: 'history',
    routes: routes
})

router.beforeEach((to, from, next) => {
    document.title = to.meta.title + " - 薪酬系统"
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
