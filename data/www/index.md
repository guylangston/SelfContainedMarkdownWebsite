# Self Contained Markdown Website

## Organisation Rules
- All `*.md` files in root are top-level nav items
- All folders in root are top-level nav times, where index.md is the default content

## Implmentation Rules
- Use a single `rootData` folder
- On startup all content is recursed and stored in memory
    - Assuming a relatively small number of files < 1000.

# Features
- Markdown rendering with Url rewriting
- Mermaid.js for diagrams
- Support for images, videos, pdf
- Auto-refresh mode with file-watching
- Bootstrap templates
- Dark/Light mode
