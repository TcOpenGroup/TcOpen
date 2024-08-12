import argparse
import os
import subprocess
import sys
import textwrap


def check_env_variable():
    stweep_key = os.getenv("STWEEP_CLI_KEY")
    if not stweep_key:
        error_message = """\
        Error: STWEEP_CLI_KEY environment variable is not set.
        Please set the STWEEP_CLI_KEY environment variable as follows:

        Unix-based systems:
        Add the following line to your .bashrc, .zshrc, or equivalent file:
        export STWEEP_CLI_KEY="your_license_key"

        Windows:
        1. Open the Start Search, type in 'env', and select 'Edit the system environment variables'.
        2. In the System Properties window, click on the "Environment Variables" button.
        3. In the Environment Variables window, click "New" under "User variables" or "System variables" and add STWEEP_CLI_KEY with your license key.
        """
        print(textwrap.dedent(error_message))
        sys.exit(1)
    return stweep_key


def activate_license(stweep_key):
    subprocess.run(
        ["STweep.CLI.exe", "license-activate", "--key", stweep_key, "--acceptEula"],
        check=True,
    )


def deactivate_license():
    subprocess.run(["STweep.CLI.exe", "license-deactivate"], check=True)


def format_files(settings_file, paths):
    subprocess.run(
        ["STweep.CLI.exe", "format", "--settingsFile", settings_file, "--path"] + paths,
        check=True,
    )


def main():
    parser = argparse.ArgumentParser(description="Format files using STweep CLI.")
    parser.add_argument(
        "--settingsFile", required=True, help="Path to the settings file"
    )
    parser.add_argument("files", nargs="+", help="Paths to the files to be formatted")

    args = parser.parse_args()

    stweep_key = check_env_variable()

    try:
        activate_license(stweep_key)
        format_files(args.settingsFile, args.files)
    finally:
        deactivate_license()


if __name__ == "__main__":
    main()
