var vote = artifacts.require("Vote");

module.exports = function(deployer) {
    deployer.deploy(vote);
};