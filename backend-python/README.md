# short readme for the backend file structure

### fly-simulator / backend / backend-python

- ML logic in charge of driving the brain

From https://male-cns.janelia.org/download/#__tabbed_3_3 download following raw data files to 'MaleCNS V1 RawData'
    -https://storage.googleapis.com/flyem-male-cns/v1.0/connectome-data/flat-connectome/connectome-weights-male-cns-v1.0-minconf-0.5.feather
    -https://storage.googleapis.com/flyem-male-cns/v1.0/connectome-data/flat-connectome/syn-points-male-cns-v1.0-minconf-0.5.feather
    -https://storage.googleapis.com/flyem-male-cns/v1.0/connectome-data/flat-connectome/syn-partners-male-cns-v1.0-minconf-0.5.feather
    -https://storage.googleapis.com/flyem-male-cns/v1.0/connectome-data/flat-connectome/tbar-neurotransmitters-male-cns-v1.0.feather
    -https://storage.googleapis.com/flyem-male-cns/v1.0/connectome-data/flat-connectome/body-neurotransmitters-male-cns-v1.0.feather
run connectome.py to create the connectome_W file which stores all neural network data
