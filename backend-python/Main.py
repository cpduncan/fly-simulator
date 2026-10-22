import torch, torch.nn as nn
import snntorch as snn

connectome_data = torch.load("connectome_W.pt", weights_only=False)
W, ids, neuron_sign = connectome_data["W"], connectome_data["ids"], connectome_data["neuron_sign"]

class TheFly(nn.Module):
    def __init__(self, W, beta=0.9, threshold=1.0, gain=0.05):
        super().__init__()
        self.W = W
        self.gain = gain                      # global scale on synaptic weights
        self.lif = snn.Leaky(beta=beta, threshold=threshold)
        self.reset()

    #used in between generations and everytime unity restarts / basically everytime we have a new fly
    def reset(self, batch=1):
        n=self.W.shape[0]

        #reset all neurons to have not spiked
        self.spike = torch.zeros(n, batch, device=self.W.device)
        #remove charge from all membranes
        membrane = torch.zeros(n, batch, device=self.W.device)

    @torch.no_grad()
    def step(self, ext_input):
        recurrent_input = torch.sparse.mm(self.W, self.spike) * self.gain
        self.spike, self.membrane = self.lif(ext_input + recurrent_input, self.membrane)
        return self.spike

#declare the Fly brain
device = "cuda" if torch.cuda.is_available() else "cpu"
model = TheFly(W.to(device)).to(device)