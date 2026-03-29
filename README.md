# JekyllNet Themes Store

[English](#english) | [中文](#中文)

---

## English

### What is This?

This is the official **JekyllNet Theme Store** — a curated collection of Jekyll themes that are fully compatible with the [JekyllNet](https://github.com/JekyllNet) platform. Every theme submitted here must be verifiable, categorized, and processable by JekyllNet without modification.

### Theme Categories

Themes **must** be placed in the correct category subdirectory under `themes/`. The available categories are:

| Category | Directory | Description |
|---|---|---|
| Blog | `themes/blog/` | Personal or professional blogging themes |
| Portfolio | `themes/portfolio/` | Showcase / portfolio themes |
| Documentation | `themes/documentation/` | Technical documentation site themes |
| Landing Page | `themes/landing/` | Single-page marketing / landing page themes |
| Minimal | `themes/minimal/` | Minimalist, lightweight themes |
| Business | `themes/business/` | Corporate / business website themes |

If your theme does not fit any existing category, open an **Issue** to request a new category before submitting a PR.

### Repository Structure

```
themes/
├── blog/             # Blog themes
├── portfolio/        # Portfolio themes
├── documentation/    # Documentation themes
├── landing/          # Landing page themes
├── minimal/          # Minimal themes
└── business/         # Business themes

.github/
├── ISSUE_TEMPLATE/
│   ├── theme-review.yml      # Report a problem with an existing theme
│   └── new-category.yml      # Request a new theme category
└── PULL_REQUEST_TEMPLATE.md  # PR template for theme submissions
```

### How to Submit a Theme

1. **Fork** this repository.
2. Place your theme files under the correct category directory: `themes/<category>/<your-theme-name>/`.
3. Ensure the theme directory contains a `README.md` describing the theme (see [Theme README Requirements](#theme-readme-requirements)).
4. Open a **Pull Request** using the provided PR template and fill in **all** required fields.
5. A maintainer will review the PR and may request changes.

### Theme README Requirements

Each theme subdirectory **must** include a `README.md` with at minimum:

- Theme name and a one-line description
- Live preview URL
- Screenshot (at least one)
- Installation instructions
- Configuration options
- License

### PR & Issue Standards

- All PRs must use the provided PR template.  
- Issues about specific themes must reference the PR number and theme name.  
- Preview sites must be live and accessible at the time of submission.  
- The submitted theme repository must be public and the git SHA pinned in the PR must match the reviewed commit.

---

## 中文

### 这是什么？

这是 **JekyllNet 主题商店** 的官方仓库，汇集了所有经过审核、可被 [JekyllNet](https://github.com/JekyllNet) 平台直接处理的 Jekyll 主题。提交到本仓库的所有主题均须经过分类管理，并满足 JekyllNet 的兼容性要求。

### 主题分类

主题**必须**放置在 `themes/` 目录下对应分类的子目录中。现有分类如下：

| 分类 | 目录 | 说明 |
|---|---|---|
| 博客 | `themes/blog/` | 个人或专业博客主题 |
| 作品集 | `themes/portfolio/` | 展示类 / 作品集主题 |
| 文档 | `themes/documentation/` | 技术文档站点主题 |
| 落地页 | `themes/landing/` | 单页营销 / 落地页主题 |
| 极简 | `themes/minimal/` | 简洁轻量主题 |
| 商业 | `themes/business/` | 企业 / 商业网站主题 |

若您的主题不适合现有分类，请先提交 **Issue** 申请新分类，再提交 PR。

### 仓库结构

```
themes/
├── blog/             # 博客主题
├── portfolio/        # 作品集主题
├── documentation/    # 文档主题
├── landing/          # 落地页主题
├── minimal/          # 极简主题
└── business/         # 商业主题

.github/
├── ISSUE_TEMPLATE/
│   ├── theme-review.yml      # 反馈已有主题的问题
│   └── new-category.yml      # 申请新的主题分类
└── PULL_REQUEST_TEMPLATE.md  # 主题提交 PR 模板
```

### 如何提交主题

1. **Fork** 本仓库。
2. 将主题文件放置在正确的分类目录下：`themes/<分类>/<主题名称>/`。
3. 确保主题目录中包含 `README.md`（参见 [主题 README 要求](#主题-readme-要求)）。
4. 使用提供的 PR 模板开启 **Pull Request**，并**完整填写**所有必填字段。
5. 维护者将审核 PR，并可能要求修改。

### 主题 README 要求

每个主题子目录**必须**包含 `README.md`，至少包含以下内容：

- 主题名称及一句话简介
- 在线预览地址
- 截图（至少一张）
- 安装说明
- 配置选项说明
- 开源许可证

### PR 与 Issue 规范

- 所有 PR 必须使用提供的 PR 模板。
- 针对特定主题的 Issue 必须注明关联的 PR 编号和主题名称。
- 提交时预览站必须在线且可正常访问。
- 提交的主题仓库必须为公开仓库，PR 中注明的 git SHA 必须与被审核的提交一致。

---

## License

This repository is licensed under the [MIT License](LICENSE).