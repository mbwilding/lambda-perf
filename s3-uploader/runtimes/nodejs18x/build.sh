DIR_NAME="./runtimes/$1"
ARCH=$2

pushd ${DIR_NAME}
yarn install
popd

rm ${DIR_NAME}/code_${ARCH}.zip 2> /dev/null
zip -r ${DIR_NAME}/code_${ARCH}.zip ${DIR_NAME}
