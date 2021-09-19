import React from "react";
import { Icon, IconButton, IconButtonProps } from "@mui/material";

interface CardIconButtonProps extends IconButtonProps {
    icon: string;
}

const CardIconButton: React.FC<CardIconButtonProps> = (props) => {
    const { icon, ...rest } = props;
    return (
        <IconButton {...rest}>
            <Icon>{icon}</Icon>
        </IconButton>
    );
};

export default CardIconButton;
