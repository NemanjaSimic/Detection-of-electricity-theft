from sklearn.ensemble import IsolationForest
import numpy as np
from numpy import genfromtxt

X = genfromtxt('C:\\Users\\admin\\Desktop\\python2\\CSVFinal.csv', delimiter = ',')

clf = IsolationForest(n_estimators=20, random_state=0, warm_start=True)
clf.fit(X)  # fit the added trees  

Y = clf.predict(X)
print(Y)