const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
var CompressionPlugin = require('compression-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    const extractCSS = new ExtractTextPlugin('vendor.css');

    return [{
        stats: { modules: false },
        resolve: { extensions: [ '.js' ] },
        entry: {
            vendor: [
                'bootstrap',
                'bootstrap/dist/css/bootstrap.css',
                'event-source-polyfill',
                'isomorphic-fetch',
                'jquery',
                'vue',
                'vue-router',
                'vue/dist/vue.js',
                // 'vue-property-decorator',
                // 'vue-class-component',
                'element-ui',
                'element-ui/lib/theme-chalk/index.css',
                '@progress/kendo-ui',
                '@progress/kendo-ui/js/messages/kendo.messages.zh-CN.js',
                '@progress/kendo-grid-vue-wrapper',
                '@progress/kendo-datasource-vue-wrapper',
                '@progress/kendo-ui/css/web/kendo.common.min.css',
                '@progress/kendo-ui/css/web/kendo.common.core.min.css',
                '@progress/kendo-ui/css/web/kendo.silver.min.css',                
                'font-awesome/css/font-awesome.min.css',
                //'fetch-download',
                'html-entities',
                'reflect-metadata',
                'fetch-intercept',
                'v-jsoneditor',
                'vue-codemirror',
                'codemirror',
                'vuedraggable'
                ,'codemirror/lib/codemirror.css'
                // language
                ,'codemirror/mode/javascript/javascript.js'
                ,'codemirror/mode/vue/vue.js'
                // theme css
                ,'codemirror/theme/monokai.css'
                // require active-line.js
                ,'codemirror/addon/selection/active-line.js'
                // styleSelectedText
                ,'codemirror/addon/selection/mark-selection.js'
                ,'codemirror/addon/search/searchcursor.js'
                // hint
                ,'codemirror/addon/hint/show-hint.js'
                ,'codemirror/addon/hint/show-hint.css'
                ,'codemirror/addon/hint/javascript-hint.js'
                // highlightSelectionMatches
                ,'codemirror/addon/scroll/annotatescrollbar.js'
                ,'codemirror/addon/search/matchesonscrollbar.js'
                
                ,'codemirror/addon/search/match-highlighter.js'
                // keyMap
                ,'codemirror/mode/clike/clike.js'
                ,'codemirror/addon/edit/matchbrackets.js'
                ,'codemirror/addon/comment/comment.js'
                ,'codemirror/addon/dialog/dialog.js'
                ,'codemirror/addon/dialog/dialog.css'
                
                ,'codemirror/addon/search/search.js'
                ,'codemirror/keymap/sublime.js'
                // foldGutter
                ,'codemirror/addon/fold/foldgutter.css'
                ,'codemirror/addon/fold/brace-fold.js'
                ,'codemirror/addon/fold/comment-fold.js'
                ,'codemirror/addon/fold/foldcode.js'
                ,'codemirror/addon/fold/foldgutter.js'
                ,'codemirror/addon/fold/indent-fold.js'
                ,'codemirror/addon/fold/markdown-fold.js'
                ,'codemirror/addon/fold/xml-fold.js'
            ],
        },
        module: {
            rules: [
                { test: /\.css(\?|$)/, use: extractCSS.extract({ use: isDevBuild ? 'css-loader' : 'css-loader?minimize' }) },
                { test: /\.(png|jpg|jpeg|gif|svg|ttf|woff|woff2|eot)(\?|$)/, use: 'url-loader?limit=100000' }
            ]
        },
        output: { 
            path: path.join(__dirname, 'wwwroot', 'dist'),
            publicPath: '/dist/',
            filename: '[name].js',
            library: '[name]_[hash]'
        },
        plugins: [
            extractCSS,
            new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery' }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
            new webpack.DefinePlugin({
                'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
            }),
            new webpack.DllPlugin({
                path: path.join(__dirname, 'wwwroot', 'dist', '[name]-manifest.json'),
                name: '[name]_[hash]'
            }),
            new CompressionPlugin({
                filename: "[path].gz[query]",
                algorithm: "gzip",
                test: /\.js$|\.css$|\.html$/,
                threshold: 10240,
                minRatio: 0.8
            }),
        ].concat(isDevBuild ? [] : [
            new webpack.optimize.UglifyJsPlugin()
        ])
    }];
};
