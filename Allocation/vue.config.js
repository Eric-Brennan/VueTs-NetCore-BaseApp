module.exports = {
    pluginOptions: {
        electronBuilder: {
            builderOptions: {
                productName: "RAMP",
                appId: 'ramp.com',
                win: {
                    "target": [
                        "nsis"
                    ],
                    icon: 'public/assets/RAMPIcon.ico',
                    "requestedExecutionLevel": "requireAdministrator"
                },
                "nsis": {
                    "installerIcon": "public/assets/RAMPIcon.ico",
                    "uninstallerIcon": "public/assets/RAMPIcon.ico",
                    "uninstallDisplayName": "Uninstall RAMP",
                    "oneClick": false,
                    "allowToChangeInstallationDirectory": true
                }
            },
        },
    },
}