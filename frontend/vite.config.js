import { defineConfig } from "vite";

export default defineConfig({
  root: "./src",
  server: {
    port: 3000,
    proxy: {
      "/api": {
        target: "http://localhost:5103",
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, ""),
      },
    },
  },
});
