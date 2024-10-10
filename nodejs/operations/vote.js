const ethers = require("ethers");
require('../utils/question')();


module.exports = function() { 
    this.vote = async function (){
        //Creates the connecto to the network
        const provider = new ethers.JsonRpcProvider("http://localhost:7545");
    
        //The contract address.
        const address = "0xF9c49E7C465609870ed6d639c82B56D6D2539095";
    
        //The ABI for the contract.
        const ABI = require("../../ABIs/vote.json");
    
        //Gets an instance of the contract.
        let voteContract = new ethers.Contract(address, ABI ,provider);
    
        let candidate1 = await voteContract.candidate1();
        console.log(`Votos Candidato 1: ${candidate1}`);
    
        let candidate2 = await voteContract.candidate2();
        console.log(`Votos Candidato 2: ${candidate2}`);
    
        accountAddress = await askQuestion('Insira o endereço da chave privada de sua carteira: ');
        
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
    
        let voteContractWithWallet = new ethers.Contract(address, ABI, wallet);
    
        vote = await askQuestion('Digite 1 para votar no candidato 1, digite 2 para votar no candidato 2: ');
    
        console.log(`Você informou ${vote}`);
    
        try{
            const gasLimit = 400000;
            const transaction = await voteContractWithWallet.castVote(vote, {gasLimit: gasLimit});
           
            console.log('Voto enviado:', transaction.hash);
    
            const receipt = await transaction.wait();
            console.log('Voto confirmado:', receipt);
    
        }catch(error){
            if (error?.error?.data?.reason)
                console.log(`Erro ao realizar o voto: ${error.error.data.reason}`);
            else
                console.log(`Erro: ${error}`);
        }
    }
}
