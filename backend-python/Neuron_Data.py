#
#This file is used as a helper function to gather input and output neuron groupings
#call Neuron_Data.find_positions(superclass)
#

import pandas
import numpy

#grab the ids data from connectome_data
connectome_data = torch.load("connectome_W.pt", weights_only=False)
ids = connectome_data["ids"]

neuron_data = pandas.read_feather('MaleCNS V1 RawData/body-annotations-male-cns-v1.0-minconf-0.5.feather')

#finds position of neurons in sparse matrix given their superclass
def find_positions(superclass):
    bodies = neuron_data.loc[neuron_data["superclass"] == superclass, "bodyId"].to_numpy(dtype="int64")
    position_in_matrix = numpy.isin(bodies, ids)
    return numpy.searchsorted(ids, bodies[position_in_matrix])