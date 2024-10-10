const ethers = require("ethers");
require('../utils/question')();


module.exports = function() { 
    this.transferEtherium = async function (){

        console.log('Iniciando transferência de ethererium');

        //Creates the connecto to the network
        const provider = new ethers.JsonRpcProvider("http://localhost:7545");
    
        accountAddress = await askQuestion('Insira o endereço da chave privada de sua carteira de origem: ');
        
        let wallet = null;
        
        // Conecta a carteira ao provedor
        try{
            wallet = new ethers.Wallet(accountAddress, provider);
            console.log(`Carteira ${accountAddress} conectada com sucesso`);
        }
        catch(error){
            console.log(`Erro ao conectar a carteira: ${error}`);
            return;
        }

        // Definir o endereço de destino
        const toAddress = await askQuestion('Insira o endereço da chave pública para a carteira de destino: ');

        const amountInEther = await askQuestion('Insira valor a ser transferido: ');

        const amount = ethers.parseEther(amountInEther);
    
        try{
            // Criar e enviar a transação
            const tx = await wallet.sendTransaction({
                to: toAddress,
                value: amount
            });

            console.log('Transação enviada:', tx.eeeeeehash);

            // Aguardar a confirmação da transação
            const receipt = await tx.wait();
            console.log('Transação confirmada:', receipt);
    
        }catch(error){
            console.log(`Erro: ${error}`);
        }
    }
}
