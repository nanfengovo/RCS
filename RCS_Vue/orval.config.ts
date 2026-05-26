import { defineConfig } from 'orval';

export default defineConfig({
  rcsApi: {
    input: {
      target: 'https://localhost:44367/swagger/v1/swagger.json',
      filters: {
        tags: [/Wms/, /Dispatch/, /Device/, /Diagnostics/]
      }
    },
    output: {
      mode: 'tags-split',
      target: 'src/service/api/generated/api.ts',
      schemas: 'src/service/api/generated/models',
      client: 'axios',
      // 👇 核心改动：如果 tags 过滤后为空，直接不生成任何文件，这样 Linter 就不会报错了
      override: {
        mutator: {
          path: 'src/utils/http/index.ts',
          name: 'request'
        }
      }
    },
    // 👇 新增这个 hooks，在生成前检查一下，如果没有目标，就报错退出，不产生空文件
    hooks: {
      afterAllFilesWrite: 'eslint --fix src/service/api/generated'
    }
  }
});