const path = require("path");

module.exports = {
  mode: 'development',
  entry: './wwwroot/src/index.js',
  output: {
    path: path.resolve(__dirname, "./wwwroot/dist"),
    filename: "[name].js"
  },
  resolve: {
    extensions: ['.js', '.jsx']
  },
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader' // will use .babelrc
        }
      },
      {
        test: /\.scss$/,
        use: [
          'style-loader',
          'css-loader',
          'sass-loader'
        ]
      },
      {
        test: /\.css$/,
        use: [
          'style-loader',
          'css-loader'
        ]
      }
    ]
  },
  devtool: 'source-map'
}