# Contributing to JekyllNet Themes Store

[English](#english) | [中文](#中文)

---

## English

Thank you for your interest in contributing to the JekyllNet Theme Store!  
Please read this guide carefully before opening a PR or Issue.

### Before You Start

- Make sure your theme works with a standard Jekyll setup and is processable by JekyllNet without modification.
- Make sure your theme is not already listed in this store.
- All theme source repositories must be **public** on GitHub (or another public git host).

### Submitting a Theme (PR)

1. **Fork** this repository and create a feature branch.
2. Add your theme files under the appropriate category:
   ```
   themes/<category>/<theme-name>/
   ```
   For example: `themes/blog/my-awesome-theme/`
3. Include a `README.md` inside your theme directory. See [Theme README Requirements](README.md#theme-readme-requirements).
4. Open a Pull Request using the **PR template** provided. Every field is required:
   - **Theme Name** — the human-readable name of your theme.
   - **Category** — the category directory your theme is placed in.
   - **Preview URL** — a live, publicly accessible preview of the theme.
   - **Theme Repository URL** — the public git repository URL for the theme source.
   - **Git SHA** — the exact commit SHA in your theme repository that this submission is based on (link to the commit on GitHub/GitLab).
   - **Description** — a brief description of the theme (features, use case, etc.).
   - **Checklist** — confirm all checklist items before submitting.

### Reporting a Theme Problem (Issue)

Use the **Theme Review** issue template. You must provide:

- The PR number that introduced the theme.
- The theme name and category.
- The preview site URL.
- A clear description of the problem.

### Category Requests (Issue)

Use the **New Category** issue template if your theme does not fit any existing category.

### Code of Conduct

Be respectful and constructive in all interactions.  
Submissions that contain malicious code, offensive content, or violate any license will be rejected immediately.

---

## 中文

感谢您有意向为 JekyllNet 主题商店做出贡献！  
在提交 PR 或 Issue 之前，请仔细阅读本指南。

### 开始之前

- 确保您的主题可以在标准 Jekyll 环境中正常运行，并且 JekyllNet 无需修改即可处理。
- 确保您的主题尚未被收录到本商店中。
- 所有主题源码仓库必须在 GitHub（或其他公开 git 托管平台）上**公开可访问**。

### 提交主题（PR）

1. **Fork** 本仓库并创建功能分支。
2. 将主题文件放置在对应分类目录下：
   ```
   themes/<分类>/<主题名称>/
   ```
   例如：`themes/blog/my-awesome-theme/`
3. 在主题目录中包含一个 `README.md`，参见 [主题 README 要求](README.md#主题-readme-要求)。
4. 使用提供的 **PR 模板**开启 Pull Request，所有字段均为必填：
   - **主题名称** — 主题的可读名称。
   - **分类** — 主题所在的分类目录名称。
   - **预览地址** — 主题的在线预览 URL（必须公开可访问）。
   - **主题仓库地址** — 主题源码的公开 git 仓库 URL。
   - **Git SHA** — 本次提交所基于的主题仓库中的精确 commit SHA（提供指向该 commit 的 GitHub/GitLab 链接）。
   - **说明** — 简短介绍主题的功能、适用场景等。
   - **检查清单** — 提交前请确认所有检查项。

### 反馈主题问题（Issue）

使用 **Theme Review（主题反馈）** Issue 模板，需提供：

- 引入该主题的 PR 编号。
- 主题名称与分类。
- 预览站 URL。
- 问题的清晰描述。

### 申请新分类（Issue）

若您的主题不适合任何现有分类，请使用 **New Category（新分类申请）** Issue 模板。

### 行为准则

请在所有交互中保持尊重和建设性态度。  
包含恶意代码、冒犯性内容或违反许可证的提交将被立即拒绝。
