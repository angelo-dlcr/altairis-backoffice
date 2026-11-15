FROM node:26.13.1-alpine AS development
WORKDIR /app

# Copiar archivos de configuración de npm
COPY package*.json ./

RUN npm install -g @angular/cli@22.2.7
RUN npm install

COPY . .

EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200", "--poll", "2000"]
