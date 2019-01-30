FROM consul:1.0.6

ENV DNS_RESOLVES consul
ENV DNS_PORT 8600

ADD ./config /config/

ENTRYPOINT ["/bin/consul", "agent", "-server", "-config-dir=/config", "-bootstrap", "-advertise=${HOST_IP}"]