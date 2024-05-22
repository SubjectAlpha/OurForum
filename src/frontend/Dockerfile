FROM node:20 AS client-build
WORKDIR /app
COPY . ./
RUN npm install
RUN npm run build

FROM nginx:1.19-alpine
COPY --from=client-build /app/public /usr/share/nginx/html