# FROM node:lts-alpine
# WORKDIR /client
# COPY package.json .
# RUN npm install
# COPY . .
# CMD ["npm", "start"]


FROM node:lts-alpine as build
WORKDIR /client
COPY package.json /client/
COPY package-lock.json /client/
RUN npm install
COPY . /client
RUN npm run build

FROM nginx:1.25.1-alpine
COPY --from=build /client/build /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx/nginx.conf /etc/nginx/conf.d
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

