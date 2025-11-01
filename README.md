# bStats Chart

A GitHub Action that generates custom chart images from [bStats.org](https://bstats.org) plugin statistics data.

## Example Output

![Example Chart](https://raw.githubusercontent.com/code-lime/charts/master/output/bstats/velocircon.png)

*Example chart generated from bStats plugin data*

## Features

- 📊 Create beautiful line charts from bStats plugin data
- 🎨 Support for multiple chart keys in a single visualization
- ⚙️ Customizable chart dimensions (width and height)
- 📅 Configurable time range (number of days)
- 🐳 Docker-based GitHub Action for easy integration

## Usage

### GitHub Actions Workflow

Based on the example from [code-lime/charts](https://github.com/code-lime/charts):

```yaml
name: Generate bStats Chart

on:
  schedule:
  - cron: "0 * * * *"
  workflow_dispatch:
  push:
    paths-ignore:
      - "output/**"

permissions:
  contents: write

jobs:
  update:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout repository
        uses: actions/checkout@v4
        with:
          persist-credentials: true

      - name: Run generation
        uses: code-lime/bstats-chart@v1
        with:
          plugin-id: 26312
          output-file: 'output/bstats/velocircon.png'

      - name: Commit updated
        uses: stefanzweifel/git-auto-commit-action@v5
        with:
          commit_message: Update output
```

## Inputs

| Input           | Default     | Description                                               |
| --------------- | ----------- | --------------------------------------------------------- |
| `plugin-id`     | Required    | Plugin ID (number) from bstats.org                        |
| `chart-keys`    | `servers`   | Chart keys separated by commas (e.g.,`servers,players`)   |
| `output-file`   | Required    | Path where the PNG image will be saved                    |
| `days`          | `100`       | Total number of days to display on the chart              |
| `width`         | `800`       | Output image width in pixels                              |
| `height`        | `200`       | Output image height in pixels                             |

## Examples

### Single Chart

```yaml
- name: Generate bStats Chart
  uses: code-lime/bstats-chart@main
  with:
    plugin-id: 12345
    chart-keys: 'servers'
    output-file: 'output/bstats/servers-chart.png'
```

### Multiple Charts

```yaml
- name: Generate bStats Chart
  uses: code-lime/bstats-chart@main
  with:
    plugin-id: 12345
    chart-keys: 'servers,players'
    output-file: 'output/bstats/stats.png'
    days: 180
    width: 1200
    height: 400
```

## License

[MIT](LICENSE)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
