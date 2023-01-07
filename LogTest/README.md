### 编译
docker rmi logtest:dev
docker build . -t logtest:dev




### 运行


#### 用完删除
docker run --rm \
-p 10080:80 \
-e "ASPNETCORE_ENVIRONMENT=Production" \
-v /home/ubuntu/LogTestSln/LogTest/Serilog.Sinks.Grafana.Loki.dll:/app/Serilog.Sinks.Grafana.Loki.dll \
-v /home/ubuntu/LogTestSln/LogTest/serilogs.Production.json:/app/serilogs.Production.json \
--name logtest \
logtest:dev

#### 后台运行
docker run -d \
-p 10080:80 \
-v /home/ubuntu/LogTestSln/LogTest/Serilog.Sinks.Grafana.Loki.dll:/app/Serilog.Sinks.Grafana.Loki.dll \
-v /home/ubuntu/LogTestSln/LogTest/serilogs.Production.json:/app/serilogs.Production.json \
--name logtest \
logtest:dev


docker exec -it logtest /bin/bash

docker rm -f logtest
