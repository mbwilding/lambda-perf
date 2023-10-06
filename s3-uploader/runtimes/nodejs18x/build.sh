DIR_NAME="./runtimes/$1"
ARCH=$2

rm ${DIR_NAME}/code_${ARCH}.zip 2> /dev/null

pushd ${DIR_NAME}
yarn install
zip -r ../code_${ARCH}.zip *
popd
