import { defineConfig } from 'orval';

export default defineConfig({
  wmsApi: {
    input: {
      target: 'https://localhost:9000/swagger/wms/swagger.json',
      filters: {
        tags: [/Location/]
      }
    },
    output: {
      mode: 'tags-split',
      target: 'src/service/api/generated/api.ts',
      schemas: 'src/service/api/generated/models',
      client: 'axios',
      override: {
        mutator: {
          path: 'src/service/request/orval.ts',
          name: 'request'
        }
      }
    }
  }
});
