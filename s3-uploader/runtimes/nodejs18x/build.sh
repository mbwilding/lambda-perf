DIR_NAME="./runtimes/$1"
ARCH=$2

yarn install
rm ${DIR_NAME}/code_${ARCH}.zip 2> /dev/null
zip -r ${DIR_NAME}/code_${ARCH}.zip ${DIR_NAME}/index.js ${DIR_NAME}/node_modules