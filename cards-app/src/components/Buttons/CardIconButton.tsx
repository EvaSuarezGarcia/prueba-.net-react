import React from "react";
import { IconButton, IconButtonProps } from "@mui/material";

interface CardIconButtonProps extends IconButtonProps {
    icon: React.ReactElement;
}

const CardIconButton: React.FC<CardIconButtonProps> = (props) => {
    const { icon, ...rest } = props;
    return (
        <IconButton {...rest}>
            {icon}
        </IconButton>
    );
};

export default CardIconButton;
