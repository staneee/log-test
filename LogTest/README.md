### 运行

docker run --rm \
-p 10080:80 \
-v /home/ubuntu/LogTestSln/LogTest/Serilog.Sinks.Grafana.Loki.dll:/app/Serilog.Sinks.Grafana.Loki.dll \
-v /home/ubuntu/LogTestSln/LogTest/serilogs.Production.json:/app/serilogs.Production.json \
--name logtest \
logtest:dev


