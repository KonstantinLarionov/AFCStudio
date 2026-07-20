Для параллельного развёртывания сайта с другим контейнером ещё одного сайта используется общий nginx
docker network create shared-proxy-network

Модификация cker-compose доп сайта:
убрать секцию ports у развёртываемого сайта заменить её на expose
expose:
      - "8080"
Это откроет порт только внутри Docker-сети, а не на самом сервере.

Подключаем к общей сети
networks:
      - default
      - shared-proxy-network

Объявляем внешнюю сеть
networks:
  shared-proxy-network:
    external: true

Обновлён docker-compose "afcstudio"
Для afcstudio и для nginxпрописывается
networks:
      - default
      - shared-proxy-network

и добавляется в конце файла
networks:
  shared-proxy-network:
    external: true

Nginx требует SSL-файлы для запуска, при первом старте он может выдать ошибку, если папки motosalvage.ru в сертификатах еще нет.
Чтобы выпустить первый сертификат, временно закомментировать (поставив # в начале строки) секцию listen 443 ssl ... для motosalvage.ru, запустить Nginx, выполнить команду генерации сертификата в контейнере certbot:

docker compose exec certbot certbot certonly --webroot -w /var/www/certbot -d motosalvage.ru -d www.motosalvage.ru

После успешного выпуска — раскомментируйте строки обратно и перезагрузите конфигурацию: 

docker compose exec nginx nginx -s reload.