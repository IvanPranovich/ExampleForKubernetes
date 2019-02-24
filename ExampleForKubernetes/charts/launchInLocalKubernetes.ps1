$chartName = 'exampleforkubernetes'
helm delete $chartName --purge
helm install exampleforkubernetes --name $chartName --wait
start 'http://localhost:30003'