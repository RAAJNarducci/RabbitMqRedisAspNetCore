FROM microsoft/mssql-server-linux
RUN mkdir -p /opt/scripts
COPY initial.sql /opt/scripts/
ENV MSSQL_SA_PASSWORD=DockerSql2017
ENV ACCEPT_EULA=Y

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $MSSQL_SA_PASSWORD \
       -i opt/scripts/initial.sql \
    && pkill sqlservr

CMD [ "/opt/mssql/bin/sqlservr" ]
# # CMD /bin/bash ./sqlcmdstartup.sh
# RUN ln -sfn /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd
# RUN sqlcmd -i opt/scripts/initial.sql -S localhost -U SA -P 'DockerSql2017'

