#
#!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
#Redundant file used to create the connectome file
#!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
#

import pandas
import numpy as np
import torch
import torch.nn as nn
import snntorch as snn

print ("Program Starting")

df = pandas.read_feather("MaleCNS V1 RawData/connectome-weights-male-cns-v1.0-minconf-0.5.feather")

#define the columns referenced in Connectome
body_pre, body_post, weight = "body_pre", "body_post", "weight"

print("Ive read the file")

#create list of all neurons and remove non-unique entries
ids = np.unique(np.concatenate([df[body_pre].values, df[body_post].values]))
id2idx = pandas.Series(np.arange(len(ids)), index=ids)

print("Ive created the unique list of ids")

#dimenion of matrix NxN
N = len(ids)

#all unique values for presynaptic postsynaptic neurons and synapse weight
pre = id2idx[df[body_pre].values].values
post = id2idx[df[body_post].values].values
w = df[weight].values.astype(np.float32)

print("Ive gathered values for the sparse matrix")

nt = pandas.read_feather("MaleCNS V1 RawData/body-neurotransmitters-male-cns-v1.0.feather")

#neurotransmitter data -> synapses will default to excitatory
sign_map = {"acetylcholine": 1, "gaba": -1, "glutamate": -1}

#filter neurons in neurotransmitter file to only one row per neuron
nt["body"] = nt["body"].astype("int64")
nt = nt.drop_duplicates("body")
nt = nt[nt["body"].isin(id2idx.index)]

#find whether neuron responds with acetylcholine gaba or glutamate
nt_sign = (nt["consensus_nt"]
             .str.lower()
             .map(sign_map)
             .fillna(1) #type unassigned by Janelia will default to +1
             .to_numpy(dtype=np.float32))

#find neuron position in id2idx
pos = id2idx[nt["body"].values].values

#assign sign to neuron
neuron_sign = np.ones(N, dtype=np.float32)
neuron_sign[pos] = nt_sign
sign = neuron_sign[pre]

print("All neurotransmitter data has been mapped")

#Create the NxN sparse matrix for all neurons
W = torch.sparse_coo_tensor(
    np.vstack([post, pre]), w * sign, size=(N, N)
).coalesce()

print("The sparse matrix has been made")

torch.save({"W": W, "ids": ids, "neuron_sign": sign}, "connectome_W.pt")

print("Sparse matrix and neuron ids have been saved to connectome_W.pt")
print("Program complete")