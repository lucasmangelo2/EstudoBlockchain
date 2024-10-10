require('./operations/vote')();
require('./operations/transfer-etherium')();

async function main() {
    //vote();
    transferEtherium();
}

main();